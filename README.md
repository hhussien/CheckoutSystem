# CheckoutSystem
If you like to run the demo sites from Visual Studio, do the followings:

For Step One:
1. Build the solution
2. When there is no build error, press F5 to run the solution. The home page should appear in the browser
3. Go to Fiddler and do a request for the service as below:
	Request URL: http://localhost:27387/api/getprice/Original
	Request Method: POST
	Request Header: Content-Type: application/json; charset=utf-8
	Request Body: {"Items":[{"BarCodeId":"111"},{"BarCodeId":"222"}]}
	
	
	*BarCodeId 111 referenced to Apple Barcode
	*BarCodeId 222 referenced to Orange Barcode
	
4. The Output will look like below in the response  body:
	TotalPrice=0.85
	
	
If you like to test the demo from Visual Studio, do the followings:

For Step One:
Test One:
1. Go to ~\CheckOutSystem\Domain.Services.Test\GetPrice\PriceCalculationTest.cs
2. Run the Test Method GetPrice()

Test Two:
1. Press F5 to start the home page
2. Go to ~\CheckOutSystem\CheckOutSystem.Tests\Controllers\GetPrice\GetPriceControllerTest.cs
3. Run the Test Method GetPrice()