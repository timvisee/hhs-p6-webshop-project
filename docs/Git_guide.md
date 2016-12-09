# How to Git on a multi-branch repo
This checklist help with using Git for a multi-branch project.

## Commit changes
Commit when you've made changes

* Check branch: Make sure you're on your own branch
* Commit changes
    * Git CLI: `git commit -m "commit message"`
    * IntelliJ: `CRTL+K`
* Push to remote repository
    * Git CLI: `git push origin <your-branch-name>`
        * For example: `git push origin dev-timvisee`
    * IntelliJ: `CRTL+SHIFT+K` (might have already pushed when committing)

## Update own branch, to get latest changes on master
Sometimes you might want to include the latest changes on the master branch, in your own branch.

* Fetch all changes from GitHub
    * Git CLI: `git fetch`
    * IntelliJ: `CRTL+SHIFT+A`, `'Fetch'`
* Rebase your branch onto the master
    * Git CLI: `git rebase origin/master`
    * IntelliJ:
        * Click Git (branch) menu on the bottom right of the IDE.
        * Click on `origin/master` in remote branches section.
        * Click `Rebase onto`
* Push your changed branch to GitHub
    * Git CLI: `git push origin <your-branch-name>`
    * IntelliJ: `CRTL+SHIFT+K`
    
## Merge your branch with master
When you've completed a feature in your own branch.
You can merge your branch with the master, to _include_ your new feature in the master project.

* Commit changes (if you haven't done that yet)
    * Git CLI: `git commit -m "commit message"`
    * IntelliJ: `CRTL+K`
* Rebase your branch onto the master
    * Git CLI: `git rebase origin/master`
    * IntelliJ:
        * Click Git (branch) menu on the bottom right of the IDE.
        * Click on `origin/master` in remote branches section.
        * Click `Rebase onto`
* Push your changed branch to GitHub
    * Git CLI: `git push origin <your-branch-name>`
        * For example: `git push origin dev-timvisee`
    * IntelliJ: `CRTL+SHIFT+K`
* Create a pull request
    * IntelliJ: `CRTL+SHIFT+A`, `'Create pull request'`
    * Other: Visit repository on GitHub, create pull request on there.
* Merge pull request.
    * Open pull request on GitHub, and press the green `Merge` button.
* Fetch all changes from GitHub
    * Git CLI: `git fetch`
    * IntelliJ: `CRTL+SHIFT+A`, `'Fetch'`
* Rebase your branch onto the master
    * Git CLI: `git rebase origin/master`
    * IntelliJ:
        * Click Git (branch) menu on the bottom right of the IDE.
        * Click on `origin/master` in remote branches section.
        * Click `Rebase onto`
* Push your changed branch to GitHub
    * Git CLI: `git push origin <your-branch-name>`
        * For example: `git push origin dev-timvisee`
    * IntelliJ: `CRTL+SHIFT+K`