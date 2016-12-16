#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Test the project, and validate the exit code
log "Testing project..."
echo "Testing is currently disabled for this project."
#dotnet test ./hhs-p6-webshop-project/project.json
#rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi

# Test finished
log "Test finished."
