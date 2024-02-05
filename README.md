.NET and EF backend for a mock real estate listing application.  
Currently configured to work with SQL Server with migrations of different versions included.  
Authentication database is separate from the main database.

Features:

- Listing Creation:
  - Options for price, currency, type and status
  - Adding Location and availability dates (location is used for displaying the listings on a map on the frontend)
  - Image attachment, which is stored as a base64 string both in full size and a compressed size for the thumbnail (compression is done in the frontend)
  - Editing or deleting owned listings

- Admin Features:
  - Adding new currency, type and status options
  - Editing or deleting any listings
 
  
