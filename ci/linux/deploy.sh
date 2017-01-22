#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Deploy the product
log "Starting Linux deployment process..."

log "Adding deployment key..."
echo "$LINUX_DEPLOY_KEY" >> ~/.ssh/authorized_keys

log "Deploying site..."
ssh -v -o StrictHostKeyChecking=no root@honeymoon.timvisee.com "~/updateSite"

# Show a success message
log "Site successfully deployed.\nURL: http://honeymoon.timvisee.com/"