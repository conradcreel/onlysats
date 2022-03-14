EMVER := $(shell yq e ".version" manifest.yaml)
ASSET_PATHS := $(shell find ./assets/*)
S9PK_PATH=$(shell find . -name onlysats.s9pk -print)

.DELETE_ON_ERROR:

all: verify

verify: onlysats.s9pk $(S9PK_PATH)
	embassy-sdk verify s9pk $(S9PK_PATH)

onlysats.s9pk: manifest.yaml LICENSE image.tar icon.png $(ASSET_PATHS)
	embassy-sdk pack

image.tar: docker_entrypoint.sh $(ASSET_PATHS) Dockerfile
	DOCKER_CLI_EXPERIMENTAL=enabled docker buildx build --platform=linux/arm64/v8  --tag start9/onlysats/main:${EMVER} -o type=docker,dest=image.tar -f ./Dockerfile . 

clean:
	rm image.tar