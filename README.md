Possible improvements

- Save album Api Urls in the Appsettings

- Make controller accept Pagination

- Add memory cache to decrease the number of calls to the 3rd party API (if the nature of the data allows it)

- Add a retry system to call the 3rd party API (use a library like Polly)

- Add authentication to the controllers (increase security)

- Use the Collections as querystring parameters

  * When calling the endpoint to get the photos, it is possible to get the photos for a specific list of albums. This may not be considered an improvement on all levels, as it comes with some tradeoffs.

  * Call all the photos: Advantage: It can be done in an async way, calling two endpoints at the same time. Disavantage:  requires more memory as we load all the photos and we use more bandwith

  * Call the photos only for the albums that we need: Advantage: We load less photos into memory. Disavantage: The calls to the endpointwould haveed to be done in a sync way as we need the list of the albums to build the queryString parameter. If we have many albums, we may reach the limit of the http request (2045 characters).

Notes about the solution

- Added swagger for your convenience
