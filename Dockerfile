# This is a manifest image, will pull the image with the same arch as the builder machine
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV LC_ALL en_US.UTF-8
RUN apt-get update \
	&& apt-get install -qq --no-install-recommends qemu qemu-user-static qemu-user binfmt-support


WORKDIR /source

# Copy solution
COPY src/onlysats.sln onlysats.sln
# Copy Web App
COPY src/onlysatsweb/onlysatsweb.csproj onlysatsweb/onlysatsweb.csproj
# Copy Domain logic
COPY src/onlysatsdomain/onlysatsdomain.csproj onlysatsdomain/onlysatsdomain.csproj
# Misc
COPY src/onlysats.tests/onlysats.tests.csproj onlysats.tests/onlysats.tests.csproj

RUN dotnet restore

COPY src/onlysatsweb/. onlysatsweb/.
COPY src/onlysatsdomain/. onlysatsdomain/.
COPY src/onlysats.tests/. onlysats.tests/.

ARG CONFIGURATION_NAME=Release
RUN dotnet publish --output /app/ --configuration ${CONFIGURATION_NAME}

# Force the builder machine to take make an arm runtime image. This is fine as long as the builder does not run any program
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm64v8
# Note: bullseye-slim (Debian 11) causes libc-bin to crash
COPY --from=builder /usr/bin/qemu-aarch64-static /usr/bin/qemu-aarch64-static
RUN apt-get update && apt-get install -y --no-install-recommends iproute2 openssh-client \
    && rm -rf /var/lib/apt/lists/* 

ENV LC_ALL en_US.UTF-8
ENV LANG en_US.UTF-8

WORKDIR /datadir
WORKDIR /app
ENV ONLYSATS_DATADIR=/datadir
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
VOLUME /datadir

COPY --from=builder "/app" .

RUN apt-get update && \
    apt-get install -y sqlite3 libsqlite3-0 curl locales jq bc wget
RUN wget https://github.com/mikefarah/yq/releases/download/v4.6.3/yq_linux_arm.tar.gz -O - |\
  tar xz && mv yq_linux_arm /usr/bin/yq

RUN locale-gen en_US.UTF-8
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV ONLYSATS_DATADIR=/datadir/onlysats
ENV LC_ALL=C 

EXPOSE 23001 80
EXPOSE 23002 443
#ADD ./configurator/target/aarch64-unknown-linux-musl/release/configurator /usr/local/bin/configurator
COPY ./docker_entrypoint.sh /usr/local/bin/docker_entrypoint.sh
COPY assets/utils/health_check.sh /usr/local/bin/health_check.sh
RUN chmod a+x /usr/local/bin/docker_entrypoint.sh
RUN chmod a+x /usr/local/bin/health_check.sh
ENTRYPOINT ["/usr/local/bin/docker_entrypoint.sh"]
