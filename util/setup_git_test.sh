#! /bin/bash

NOGIT="/tmp/nogit"
GIT="/tmp/gittest"

function init_git () {
    rm -fR $GIT
    mkdir $GIT
    cd $GIT
    git init
}

function init_no_git () {
    rm -fR $NOGIT
    mkdir $NOGIT
}

init_git 

init_no_git 

