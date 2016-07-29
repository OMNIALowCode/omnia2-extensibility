## Folder Contents
This folder contains the code samples developed for extensibility in the Version 1 of the extensibility engine, using JavaScript.

## File Descriptions

### CopyLine.js
This script is triggered from a transactional entity line in the details of an Interaction, on user request (for example, from a button). There, it creates a new line, with all the same attributes.

### RedirectToInteraction.js
This script is triggered from anywhere in the UI, and it redirects you to the creation of an interaction, with optional parameters for that creation. The same logic can also be adapted for any other operation other than creating an interaction.
