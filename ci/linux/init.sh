#!/usr/bin/env bash

# Initialization message
echo "----------------------------------------------------------------"
echo "  dotnet core CI build tool for Linux"
echo "  Written by Tim Visee, timvisee.com"
echo "----------------------------------------------------------------"
echo ""

# Set the proper execution permissions for the scripts
sudo chmod 777 ./setup.sh
sudo chmod 777 ./build.sh
sudo chmod 777 ./test.sh
