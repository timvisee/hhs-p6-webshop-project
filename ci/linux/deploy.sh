#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Deploy the product
log "Starting Linux deployment process..."

# Install required sshpass application
log "Installing sshpass to access deployment server..."
sudo apt-get -y install sshpass

# Actually deploy the site
log "Connecting Linux deployment server and deploying site..."
sshpass -p "$LINUX_DEPLOY_PASS" ssh -o StrictHostKeyChecking=no root@honeymoon.timvisee.com "~/updateSite"

# Show a success message
log "Site successfully deployed.\nURL: http://honeymoon.timvisee.com/"