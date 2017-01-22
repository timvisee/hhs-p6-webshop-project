#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Testing deployment configuration
log "Testing deployment configuration..."

if [ "$CIRCLE_BRANCH" = "master" ]
then
    # Install required sshpass application
	log "Installing sshpass to access deployment server..."
	sudo apt-get -y install sshpass

	# Actually deploy the site
	log "Connecting Linux deployment server and deploying site..."
	sshpass -p "$LINUX_DEPLOY_PASS" ssh -o StrictHostKeyChecking=no root@honeymoon.timvisee.com "~/updateSite"

	# Show a success message
	log "Site successfully deployed!\nURL: http://honeymoon.timvisee.com/"

else
	log "Not on the 'master' branch, skipping deployment!"
	exit 0
fi


