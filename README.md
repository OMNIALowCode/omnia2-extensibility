# OMNIA Platform Extensibility
This repository contains code samples for the **OMNIA Platform**'s extensibility engine. The OMNIA platform is a Microsoft Azure cloud-based platform designed for agile development and operation of Management Information Systems (MIS), enabling a no code development of solutions with rapid prototyping, in a domain-specific language, based on accounting theory and oriented for business professionals. Please access our wiki, at https://github.com/numbersbelieve/omnia/wiki, for more information.

On top of that business-oriented structure, the platform supports various types of scripting-based extensibility, allowing developer to add flexibility to the developed solutions.

## Repository structure
There are two different kinds of scripts contained within this repository:

**Integration** scripts run on an external system, such as an ERP, and are designed to perform communication between the platform and this external system: Integrating documents, modifying documents on the platform based on data from the system, reading data from the system... They are developed in C#, and the platform provides a Visual Studio solution to aid in their development and testing.

It is possible to utilize the full power of the C# references, by referencing DLLs that exist in the machine where the connector is installed.

**Extensibility** scripts run directly on the platform, enhancing its standard behaviours. An extensibility script can be, for example, a way to link from one document to a document that depends on it, or a script that creates additional lines in a document.

Currently, with the second version of the extensibility engine, these scripts can also be written in C#, utilizing the same Visual Studio solution. However, some legacy support still exists for the V1 of this engine, which uses JavaScript for its scripts.

C# scripts that run in the cloud have very limited support for DLL importing, and performance is not guaranteed as soon as you import any of the available DLLs. Some of the example scripts resort to these DLLs (`System.Web.*, System.Net.*`).

On top of the script, this repository also includes **Web Components**, another part of the platform's extensibility, that allows users to define HTML and associated JavaScript in order to extend the UI of the platform (e.g. including maps, or embedding a website).

## How to run
To execute any of these scripts, an account on a subscription based on the OMNIA platform is necessary. In the account, the user needs to access the Scripting section, define the moment when they want the script to execute, and validate its execution. Integration scripts require a connection to a valid external system, using a connector on the external system to communicate with the platform.

The platform's manuals are available [on our wiki](https://github.com/numbersbelieve/omnia/wiki/Platform-Manuals).

## Contributing and Feedback
Everyone is free to contribute their developed scripts to the repository. Our contributing policy is described in the Wiki of this project.

Any bugs detected in the scripts can be reported in the Issues section of this repository.

## License
Unless otherwise specified, the code samples are released under the MIT license.
