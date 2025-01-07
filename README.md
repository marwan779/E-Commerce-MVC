# **E-Commerce Bookstore**

An online platform for selling books, built using .NET Core MVC. This application allows customers to browse, search, and purchase books, while providing administrators with tools to manage inventory and customer orders.  

---

## **Features**

### **For Customers**
- Browse a catalog of books by category, title, or author.
- Advanced search functionality for finding specific books.
- Add books to a shopping cart and modify quantities.
- Secure checkout process for finalizing orders.
- User authentication and registration with role-based access.
- View and manage personal order history.

### **For Administrators**
- Full control over inventory: add, update, or delete book entries.
- View and process customer orders.
- Manage customer data and user roles.
- Admin dashboard for an overview of application metrics.

---

## **Technology Stack**
- **Framework**: .NET Core MVC
- **Programming Language**: C#
- **Frontend**: Razor Pages, HTML5, CSS3, JavaScript
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Version Control**: GitHub

---

## **Project Structure**
```plaintext
|-- Controllers
|   |-- CategoryController.cs        
|   |-- CompanyController.cs     
|   |-- OrderController.cs         
|   |-- ProductController
|   |-- CartController
|   |-- HomeController
|
|-- Models
|   |-- Product.cs                   
|   |-- ApplicationUser.cs                   
|   |-- OrderHeader.cs
|   |-- OrderDetail.cs                 
|   |-- ShoppingCart.cs
|   |-- ErrorViewModel.cs
|   |-- Company.cs    
|   |-- Category.cs    


```

---

## **How to Run the Project**

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/marwan779/E-Commerce-MVC.git
   cd E-Commerce-MVC
   ```

2. **Install Dependencies**:
   Ensure you have the following installed:
   - .NET SDK
   - SQL Server
   - Visual Studio (for development)

3. **Update Database**:
   - Open the terminal in your project folder.
   - Run the following commands to update the database:
     ```bash
     dotnet ef database update
     ```

4. **Run the Application**:
   - Use the terminal or Visual Studio to run the application:
     ```bash
     dotnet run
     ```

5. **Access the Application**:
   - Open your browser and navigate to `https://localhost:5001`.

---

## **Database Design**

1. **Products Table**
Id (Primary Key): Unique identifier for each book.
Title: Name of the book.
Author: Author of the book.
CategoryId (Foreign Key): Links to the Categories table.
Description: Detailed description of the book.
Price: Price of the book.
ImageUrl: URL of the book's cover image.
ISBN: International Standard Book Number.
StockQuantity: Number of available copies.
Publisher: Name of the book's publisher.
PublicationDate: Date of publication.

2. **Categories Table**
Id (Primary Key): Unique identifier for each category.
Name: Name of the category.
DisplayOrder: Determines the display order for categories in the UI.

3. **ApplicationUsers Table**
Id (Primary Key): Unique identifier for each user.
Name: Full name of the user.
Email: Email address (used for login and notifications).
PasswordHash: Securely hashed password.
Role: User role (e.g., Admin, Customer).
PhoneNumber: Contact number for the user.
Address: User's address.

5. **OrdersHeaders Table**
OrderId (Primary Key): Unique identifier for each order.
UserId (Foreign Key): Links to the Users table.
OrderDate: Date and time when the order was placed.
TotalAmount: Total cost of the order.
OrderStatus: Current status (e.g., Pending, Shipped, Delivered).
PaymentMethod: Method used for payment (e.g., Credit Card, Cash on Delivery).

6. **OrderDetails Table**
OrderDetailId (Primary Key): Unique identifier for each order item.
OrderId (Foreign Key): Links to the Orders table.
BookId (Foreign Key): Links to the Books table.
Quantity: Number of copies of the book ordered.
Price: Price of the book at the time of order.

8. **ShoppingCart Table**
CartId (Primary Key): Unique identifier for each cart entry.
UserId (Foreign Key): Links to the Users table.
BookId (Foreign Key): Links to the Books table.
Quantity: Number of copies of the book added to the cart.
Entity Relationships
Books and Categories: A book belongs to one category, but a category can have many books (one-to-many relationship).
Users and Orders: A user can place many orders, but each order belongs to one user (one-to-many relationship).
Orders and OrderDetails: An order can contain multiple order details, but each detail belongs to one order (one-to-many relationship).
Books and OrderDetails: A book can appear in multiple order details, and an order detail refers to one book (many-to-one relationship).
Users and ShoppingCart: A user can have multiple entries in their shopping cart (one-to-many relationship).

---

## **Admin Dashboard**
The Admin Dashboard includes:
- **Book Management**: Add, edit, delete books.
- **Order Management**: View all orders, update order statuses.
- **User Management**: Manage customers and roles.

---

## **Key Highlights**
- **Responsive Design**: Optimized for desktops, tablets, and mobile devices.
- **Security**:
  - Passwords securely hashed using ASP.NET Identity.
  - Role-based access for Admins and Customers.
- **Scalability**:
  - Modular design allows for easy feature extension.
  - Database migrations ensure smooth schema updates.

---

## **Future Improvements**
- **Payment Integration**: Add gateways like PayPal or Stripe for secure transactions.
- **User Profiles**: Allow users to manage account details.
- **Recommendations**: Implement AI/ML-based book recommendations.
- **Reviews**: Enable customers to review and rate books.

---

## **Contributing**
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature/bugfix.
3. Submit a pull request with a detailed description of your changes.

---



