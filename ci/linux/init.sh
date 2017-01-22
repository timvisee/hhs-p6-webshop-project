#!/usr/bin/env bash

# Initialization message
echo "----------------------------------------------------------------"
echo "  dotnet core CI build tool for Linux"
echo "  Written by Tim Visee, timvisee.com"
echo "----------------------------------------------------------------"
echo ""

# Set the proper execution permissions for the scripts
sudo chmod 777 ./ci/linux/setup.sh
sudo chmod 777 ./ci/linux/build.sh
sudo chmod 777 ./ci/linux/test.sh
sudo chmod 777 ./ci/linux/deploy.sh
