## Folder Contents
This folder contains the code samples developed for integration on the Primavera ERP.

## File Descriptions

### RemoteQuery.cs
The standard file used to obtain queries to the Primavera ERP. This script receives a SQL query string from the system, as well as all the information necessary to authenticate itself with a specific company in the ERP, and returns the result of the SQL query.

### PurchaseOrder.cs
A sample integration in the Primavera ERP. When an interaction is saved or approved in the platform, integrates a "Compras" document of type "ECF".