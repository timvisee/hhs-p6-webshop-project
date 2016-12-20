[![Build status on Circle CI in master branch](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/master.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/master)
[![Build status on Circle CI in last commit](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)

# HHS P6 Webshop Project
C#/ASP.NET webshop collaboration project for college (HHS) period 6.

## Requirements
This project is built using Mono, as crossplatform implementation for Microsoft's language.
Required packages might be installed automatically, if not, the command `dotnet restore` can be used.

If building and/or running the project fails, you may need to install the following dependencies manually:
* [Mono](http://www.mono-project.com/) [[Download]](http://www.mono-project.com/download/) (not Windows' default build environment)
* Additional requirements on Windows:
    * IIS [[Download]](https://www.microsoft.com/en-us/download/details.aspx?id=48264)
* If you get a HTTP 500 error, you can try the following command:
    `dotnet ef database update`

## Commandline Options
The following command line arguments are supported:
* `--db-init-skip`: Always skips the 3 second grace period in which the database can be reset
* `--db-init-force`: Forces database initialization every time
* `--db-init-exit`: Exits the application after the database has been initialized

	
## Build
The project is currently being built automatically using the CI services in the table below.

|Platform|Branch|Build Status|Service||
|:---|:---:|:---:|:---|---|
|Linux|master|[![Build status on Circle CI](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/master.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|CircleCI|[View Status (private)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/master)|
|Linux|last commit|[![Build status on Circle CI](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|CircleCI|[View Status (private)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|
