#! /bin/bash

NOGIT="/tmp/nogit"
GIT="/tmp/gittest"


function init_git () {
    rm -fR $GIT
    mkdir $GIT
    cd $GIT
    git init

    echo "asdf" > existing.txt
    git add existing.txt
    echo "asdf asfasg " > existing2.txt
    git add existing*

    echo "test" > changed.txt
    git add changed.txt
    echo "test2" > changed2.txt
    git add changed2.txt

    git commit -m "For unittesting"

    echo "foo bar" >> changed.txt
    echo "foo bar" >> changed2.txt
}

function init_no_git () {
    rm -fR $NOGIT
    mkdir $NOGIT
}

init_git 

init_no_git 

