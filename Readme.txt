Notes on how I approached and tested this solution (Visual Studio 2019):

I Created a .NET Solution and Project called OnlineMortgageCalculator using the .NET 4.7.2 Framework.

I created a Login and Register function for users. Can be test to click on 'Login' and 'Register' on Home page.

I used http://en.wikipedia.org/wiki/Mortgage_calculator#Monthly_payment_formula and http://www.mtgprofessor.com/formulas.htm as reference for the monthly payment formula.

Used javascript and jQuery that calculates the monthly mortgage payments for a fixed interest rate on the Home/Index.cshtml page.

Tested UI on Chrome browsers.

Compared monthly payment results to other Mortgage Calculators on the Web (https://www.betterbond.co.za/calculators/home-loan-repayment?gclid=Cj0KCQiA0eOPBhCGARIsAFIwTs4TrD4S54xSZn757O8Rx03o_Bn15kK3Cic9vMT2t3JKBO_ojuSQOmMaAk0REALw_wcB).

Further functionality could be added, such as showing the total interest paid over the loan term and catering for additional payments per month.

Automated testing of the UI, using Visual Studio Test Professional or a tool like Selenium, would provide another level of testing to ensure the application works as expected.

SQL DATABASE:
I used SQL 2019
SQL Script needed to create DB LoginRegistration: Create_LoginUserDB_Script.sql
Change Web.config - source
 <connectionStrings>
    <add name="LoginRegistrationEntities" connectionString="metadata=res://*/Models.DataModel.csdl|res://*/Models.DataModel.ssdl|res://*/Models.DataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=MICHAEL-LAPTOP;initial catalog=LoginRegistration;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>

NOTE:
I budget for about 4 hours and did not had time to implement an API solution.
