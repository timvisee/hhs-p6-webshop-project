#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Testing deployment configuration
log "Testing deployment configuration..."

# Ensure we're on the master branch
if [ "$CIRCLE_BRANCH" = "master" ]
then
    echo "OK: We're on the 'master' branch."
else
	echo "ERR: Not on the 'master branch'."
	log "Not on the 'master' branch, skipping deployment!"
	exit 0
fi

# Ensure an SSH user and host is given
if [ "$LINUX_DEPLOY_SSH_USER_HOST" = "" ]
then
    echo "OK: SSH user and host for deployment is specified in environment variable."
else
	echo "ERR: SSH user and host not specified for deployment."
	log "The SSH user and host is not specified for deployment.\nSet the environment variable 'LINUX_DEPLOY_SSL_USER_HOST'\nwith the format 'root@host' to automatically deploy.\nSkipping automatic deployment."
	exit 0
fi

# Install required sshpass application
log "Installing sshpass to access deployment server..."
sudo apt-get -y install sshpass

# Actually deploy the site
log "Connecting Linux deployment server and deploying site..."
sshpass -p "$LINUX_DEPLOY_PASS" ssh -o StrictHostKeyChecking=no "$LINUX_DEPLOY_SSH_USER_HOST" "~/updateSite"

# Show a success message
log "Site successfully deployed!\nURL: http://honeymoon.timvisee.com/"