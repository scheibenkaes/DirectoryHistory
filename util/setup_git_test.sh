#! /bin/bash

NOGIT="/tmp/nogit"
GIT="/tmp/gittest"

function init_git () {
    rm -R $GIT
    mkdir $GIT
    cd $GIT
    git init
}

function init_no_git () {
    rm -R $NOGIT
    mkdir $NOGIT
}

init_git 

init_no_git 

