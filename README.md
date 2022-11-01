# CarsDealer

Cars dealer is a web application for Angular course at SoftUni. The server is built on ASP.NET CORE 5.0 and it is using SQL Server for storing the data. The front end relies on Angular 14. The application consists of roles (user and administrator).
Each user can register, login and logout. 
Administrator can approve or decline newly added cars by user.
If user change something about it's car, admin must approve/decline the car again.
In home tab users that are authenticated could search cars by brand or model.
There are notifications that user can see. Notification is send to user when he creates/edits car or gets an offer for his car.
In menu tab offers, user can accept or decline other user's offer for given car. User can not send offers to himself. If user accept the offer, car is removed from app.
User can delete only his cars. If he tries to delete another car, he will get an error.
User that is not an admin could not open admin view.

## Built With

* ASP.Net Core 5.0
* Angular 14
* MSSQL Server

## Author
<a href="https://github.com/toshinto" title="My Profile">Todor Georgiev</a>
