## Contribution Guidelines

To uphold the quality and consistency of the `RhythmiVerse` codebase, we've implemented a structured workflow. Below are detailed instructions to guide you through the contribution process, as well as information on how to access and update our project management and documentation resources.

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

### Linking PRs to Todo Lists

Our Todo lists are located in the repository under `ProjectManagement/TodoLists/README.md`. Each weekly [Todo list](https://github.com/Aarsh2001/RythmiVerse/blob/main/ProjectManagement/TodoLists/README.md) has a corresponding issue number in our repository's Issues, to which one of us are assigned ([Aarsh](https://github.com/Aarsh2001), [Tomas](), [Dennis](https://github.com/d3utu), [Jiaming](https://github.com/RuiFan6)).

- When creating PRs that address tasks from the Todo lists, please ensure that you reference the appropriate issue number in your PR description by including `Fixes #issue_number`. This allows us to automatically link your PR to the issue and mark it as complete upon the PR's merge.

### Updating Meeting Notes

Meeting Notes can be found under `Documentation/MeetingNotes/`. They are named following the convention `Week_X_Notes.txt`, where `X` is the week number.

- To update Meeting Notes, please edit the relevant `Week_X_Notes.txt` file with your updates. If you're adding new notes for a meeting, ensure to follow the naming convention and place the file in the correct directory.
- Commit your changes to the notes with a clear message describing the update, then push the changes to your branch and create a PR to merge them into `main`. In your PR, please briefly describe the meeting's highlights and any decisions made.

Thank you for your commitment to contributing to `RhythmiVerse`. Your collaborative efforts are what make this project a success.
