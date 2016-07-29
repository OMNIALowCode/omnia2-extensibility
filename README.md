# MyMIS Platform Extensibility
This repository contains code samples for the **MyMIS Platform**'s extensibility engine. The MyMIS platform is a Microsoft Azure cloud-based platform designed for agile development and operation of Management Information Systems (MIS), enabling a no code development of solutions with rapid prototyping, in a domain-specific language, based on accounting theory and oriented for business professionals.

On top of that business-oriented structure, the platform supports various types of scripting-based extensibility, allowing developer to add flexibility to the developed solutions.

## Repository structure
There are two different kinds of scripts contained within this repository:

**Integration** scripts run on an external system, such as an ERP, and are designed to perform communication between the platform and this external system: Integrating documents, modifying documents on the platform based on data from the system, reading data from the system... They are developed in C#, and the platform provides a Visual Studio solution to aid in their development and testing.

**Extensibility** scripts run directly on the platform, enhancing its standard behaviours. An extensibility script can be, for example, a way to link from one document to a document that depends on it, or a script that creates additional lines in a document.

Currently, we are developing a new version of our extensibility engine, which will allow for their development to be done in C#. However, support still exists for the V1 of this engine, which uses JavaScript for its scripts.

## How to run
To execute any of these scripts, an account on a subscription based on the MyMIS platform is necessary. In the account, the user needs to access the Extensibility section, define the moment when they want the script to execute, and validate its execution. Integration scripts require a connection to a valid external system, using a connector on the external system to communicate with the platform.

## Contributing and Feedback
Everyone is free to contribute their developed scripts to the repository. Our contributing policy is described in the Wiki of this project.

Any bugs detected in the scripts can be reported in the Issues section of this repository.

## License
Unless otherwise specified, the code samples are released under the MIT license.
