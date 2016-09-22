## Folder Contents
This folder contains the code samples developed for extensibility in the Version 1 of the extensibility engine, using JavaScript.

## File Descriptions

### CopyLine.js
This script is triggered from a transactional entity line in the details of an Interaction, on user request (for example, from a button). There, it creates a new line, with all the same attributes.

### RedirectToInteraction.js
This script is triggered from anywhere in the UI, and it redirects you to the creation of an interaction, with optional parameters for that creation. The same logic can also be adapted for any other operation other than creating an interaction.

### LoadLinesFromERP.js
This script is triggered from the UI in an interaction, and it loads lines to it from a query to an external system.