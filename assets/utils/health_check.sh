#!/bin/bash

check_web(){
    DURATION=$(</dev/stdin)
    if (($DURATION <= 30000 )); then 
        exit 60
    else
        curl --silent --fail onlysats.embassy:23001 &>/dev/null
        RES=$?
        if test "$RES" != 0; then
            echo "The Sample MVC UI is unreachable" >&2
            exit 1
        fi
    fi
}

case "$1" in
	web)
        check_web
        ;;
    *)
        echo "Usage: $0 [command]"
        echo
        echo "Commands:"
        echo "         web"
esac