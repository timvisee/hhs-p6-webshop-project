#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Set up the build environment
log "Setting up build environment...\nSetting up dotnet repository..."

# Set up the dotnet repository
sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893

# Update all repositories
log "Updating all repositories..."
sudo apt-get update

# Install dotnet
log "Installing dotnet..."
sudo apt-get -y install dotnet-dev-1.0.0-preview2.1-003177

# Install bower
log "Installing bower..."
sudo npm install -g bower

# Install gulp
log "Installing gulp..."
sudo npm install -g gulp

# Install project dependencies
log "Installing project dependencies..."
dotnet restore

# Installing NPM depenendencies
log "Installing NPM dependencies..."
sudo npm install

# Install bower dependencies
log "Installing bower dependencies..."
sudo bower install

# Minify using gulp
log "Cleaning and minifying using gulp..."
gulp clean
gulp min

# Build environment configured
log "Build environment configured."