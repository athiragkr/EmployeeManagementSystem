Employee Management System


The feature of this system are:
Users can add, edit, view and remove employee details.
Users can export CSV for employee details.
Employee details are exposed through RESTful service https://gorest.co.in/public-api/.
For paged result search the record by passing page Number. If non
existing page number is passed, then last page results will be returned.
Created reusable code for GET, POST ,DELETE and PUT operations.
Created Unit Tests for Create New user and Get users.


1.	Add employee information:
User can add the following details and exposed into the Rest api.
    * Employee Full Name
    * Email Id
    * Gender
    * Status

2. Edit and Delete:
Be able to edit and delete employees from the Rest API. An employee's details are automatically populated in the textboxes when the user clicks on the item on datagridview and then user can update or delete the record.

3. Search:
Be able to search (an) employee(s) by passing the page No. If non
existing page number is passed, then last page results will be returned.


4. Display employees:
Be able to retrieve employees from a REST API and display them.

5.Export To Excel:
Users can export CSV for employee details.

Additional packages Installed:
Nancy
Newtonsoft.Json
Microsoft.Office.Interop.Excel

