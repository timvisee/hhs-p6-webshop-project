#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Deploy the product
log "Deploying on server..."

ssh root@honeymoon.timvisee.com "~/updateSite"

# Show a success message
log "Site successfully deployed.\nURL: http://honeymoon.timvisee.com/"