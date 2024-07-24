### Workflow for Making Contributions

1. **Fork and Clone:**
   Start by forking the repository. Afterward, clone the fork to your local development environment.

2. **Set Upstream Remote:**
   Link your local repository to the main repository using:
   ```sh
   git remote add upstream git@github.com:Aarsh2001/RythmiVerse.git
   ```

3. **Create a New Branch:**
   Always create a new branch from the latest `main` branch for your work:
   ```sh
   git checkout -b <your-branch-name>
   ```

4. **Implement Your Changes:**
   Make your code changes on your new branch. Stage and commit your changes:
   ```sh
   git add .
   git commit -m "<your-commit-message>"
   ```

5. **Push Your Branch:**
   Push your branch to your GitHub fork:
   ```sh
   git push origin <your-branch-name>
   ```

6. **Open a Pull Request:**
   When you're ready to merge your changes into the `main` branch, create a pull request (PR) from your branch on GitHub. Include a detailed description of your changes.

**Note** -: All application related files should be put inside `Assets` folder. Before making any changes, please go through the [directory structure](https://github.com/Aarsh2001/RythmiVerse/tree/main/Assets) and add your files accordingly !
