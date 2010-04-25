#! /bin/bash

NOGIT="/tmp/nogit"
GIT="/tmp/gittest"

test_dir="util/test_repo"

function init_git () {
    if [ -d "/tmp/test_repo" ];
    then
        rm -fR "/tmp/test_repo"
    fi
    cp -fR $test_dir /tmp
}

function init_no_git () {
    rm -fR $NOGIT
    mkdir $NOGIT
}

init_git 

init_no_git 

