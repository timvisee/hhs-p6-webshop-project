#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Test the project, and validate the exit code
log "Testing project..."
dotnet test ./hhs-p6-webshop-project-test/project.json
rc=$?
if [[ $rc != 0 ]]
then
    log "Testing project failed! Exited with code $rc"
    exit $rc
fi

# Test finished
log "Test finished."
