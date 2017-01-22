#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Deploy the product
log "Starting Linux deployment process..."

log "Deploying site..."
sshpass -p "$LINUX_DEPLOY_PASS" ssh -v -o StrictHostKeyChecking=no root@honeymoon.timvisee.com "~/updateSite"

# Show a success message
log "Site successfully deployed.\nURL: http://honeymoon.timvisee.com/"