using LINQques;

namespace LINQques
{
    class Student
    {
        public int Id;
        public string Name;
        public string Class;
        public int Marks;
    }
    class Employee
    {
        public int Id;
        public string Name;
        public string Dept;
        public double Salary;
        public DateTime JoinDate;
    }
    class Product
    {
        public int Id;
        public string Name;
        public string Category;
        public double Price;
    }
    class Sale
    {
        public int ProductId;
        public int Qty;
    }
    class Book
    {
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public double Price;
    }

    class Customer
    {
        public int Id;
        public string Name;
        public string City;
    }

    class Order
    {
        public int OrderId;
        public int CustomerId;
        public double Amount;
    }

    class Movie
    {
        public string Title;
        public string Genre;
        public double Rating;
        public int Year;
    }

    class Transaction
    {
        public int Acc;
        public double Amount;
        public string Type;
    }

    class CartItem
    {
        public string Name;
        public string Category;
        public double Price;
        public int Qty;
    }

    class User
    {
        public int Id;
        public string Name;
        public string Country;
    }

    class Post
    {
        public int UserId;
        public int Likes;
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
            {
                new Student{Id=1, Name="Amit", Class="10A", Marks=85},
                new Student{Id=2, Name="Neha", Class="10A", Marks=72},
                new Student{Id=3, Name="Rahul", Class="10B", Marks=90},
                new Student{Id=4, Name="Pooja", Class="10B", Marks=60},
                new Student{Id=5, Name="Kiran", Class="10A", Marks=95}
            };
            var employees = new List<Employee>
            {
                new Employee{Id=1, Name="Ravi", Dept="IT", Salary=80000, JoinDate=new DateTime(2019,1,10)},
                new Employee{Id=2, Name="Anita", Dept="HR", Salary=60000, JoinDate=new DateTime(2021,3,5)},
                new Employee{Id=3, Name="Suresh", Dept="IT", Salary=120000, JoinDate=new DateTime(2018,7,15)},
                new Employee{Id=4, Name="Meena", Dept="Finance", Salary=90000, JoinDate=new DateTime(2022,9,1)}
            };

            var products = new List<Product>
            {
                new Product{Id=1, Name="Laptop", Category="Electronics", Price=50000},
                new Product{Id=2, Name="Phone", Category="Electronics", Price=20000},
                new Product{Id=3, Name="Table", Category="Furniture", Price=5000}
            };

            var sales = new List<Sale>
            {
                new Sale{ProductId=1, Qty=10},
                new Sale{ProductId=2, Qty=20}
            };

            var books = new List<Book>
            {
                new Book{Title="C# Basics", Author="John", Genre="Tech", Year=2018, Price=500},
                new Book{Title="Java Advanced", Author="Mike", Genre="Tech", Year=2016, Price=700},
                new Book{Title="History India", Author="Raj", Genre="History", Year=2019, Price=400}
            };
            var customers = new List<Customer>
            {
                new Customer{Id=1, Name="Ajay", City="Delhi"},
                new Customer{Id=2, Name="Sunita", City="Mumbai"}
            };

            var orders = new List<Order>
            {
                new Order{OrderId=1, CustomerId=1, Amount=20000},
                new Order{OrderId=2, CustomerId=1, Amount=40000}
            };
            var movies = new List<Movie>
            {
                new Movie{Title="Inception", Genre="SciFi", Rating=9, Year=2010},
                new Movie{Title="Avatar", Genre="SciFi", Rating=8.5, Year=2009},
                new Movie{Title="Titanic", Genre="Drama", Rating=8, Year=1997}
            };
            var transactions = new List<Transaction>
            {
                new Transaction{Acc=101, Amount=5000, Type="Credit"},
                new Transaction{Acc=101, Amount=2000, Type="Debit"},
                new Transaction{Acc=102, Amount=10000, Type="Debit"}
            };
            var cart = new List<CartItem>
            {
                new CartItem{Name="TV", Category="Electronics", Price=30000, Qty=1},
                new CartItem{Name="Sofa", Category="Furniture", Price=15000, Qty=1}
            };

            var users = new List<User>
            {
                new User{Id=1, Name="A", Country="India"},
                new User{Id=2, Name="B", Country="USA"}
            };

            var posts = new List<Post>
            {
                new Post{UserId=1, Likes=100},
                new Post{UserId=1, Likes=50}
            };




            var topThreeStudents = students.OrderByDescending(x => x.Marks).Take(3);
            foreach (var student in topThreeStudents)
            {
                Console.WriteLine(student.Name + " " + student.Marks);
            }
            Console.WriteLine();
            var averageMarks = students.GroupBy(x => x.Class).Select(x => new
            {
                Class = x.Key,
                Average = x.Average(x => x.Marks)
            });
            foreach (var avg in averageMarks)
            {
                Console.WriteLine(avg.Class + " " + avg.Average);
            }

            //var belowClassAverage = students.Where(x => x.Marks < x.averageMarks);
            //foreach (var avg in averageMarks)
            //{
            //    Console.WriteLine("Student below Average in class " + avg.Class);
            //    var classAvg = students.Where(x => x.Class == avg.Class && x.Marks < avg.Average);
            //    foreach (var item in classAvg)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
            //}               //this works but not a good query

            var belowClassAverage = students.GroupBy(x => x.Class).Select(x => new
            {
                Class = x.Key,
                Stu = x.Where(y => y.Marks < x.Average(x => x.Marks))
            });

            foreach (var ab in belowClassAverage)
            {
                Console.WriteLine(ab.Class);
                foreach (var student in ab.Stu)
                {
                    Console.WriteLine(student.Name);
                }
            }

            var orderClassMark = students.OrderBy(x => x.Class).OrderByDescending(x => x.Marks);
            foreach (var val in orderClassMark)
            {
                Console.WriteLine(val.Name + " " + val.Class + " " + val.Marks);
            }


            var salaryLevel = employees.GroupBy(e => e.Dept).Select(g => new
            {
                Dept = g.Key,
                Highest = g.Max(e => e.Salary),
                Lowest = g.Min(e => e.Salary)
            });

            foreach (var s in salaryLevel)
            {
                Console.WriteLine(s.Dept + " High:" + s.Highest + " Low:" + s.Lowest);
            }


            var employeeCount = employees.GroupBy(e => e.Dept).Select(g => new
            {
                Dept = g.Key,
                Count = g.Count()
            });

            foreach (var e in employeeCount)
            {
                Console.WriteLine(e.Dept + " Count:" + e.Count);
            }


            var joinedAfter2020 = employees.Where(e => e.JoinDate.Year > 2020);

            foreach (var e in joinedAfter2020)
            {
                Console.WriteLine(e.Name);
            }

            var annualSalary = employees.Select(e => new
            {
                e.Name,
                AnnualSalary = e.Salary * 12
            });

            foreach (var e in annualSalary)
            {
                Console.WriteLine(e.Name + " " + e.AnnualSalary);
            }

            var joinedData = products.Join(sales, p => p.Id, s => s.ProductId, (p, s) => new
            {
                p.Name,
                TotalRevenue = p.Price * s.Qty
            });

            foreach (var r in joinedData)
            {
                Console.WriteLine(r.Name + " " + r.TotalRevenue);
            }




            var bestProduct = joinedData.OrderByDescending(x => x.TotalRevenue).FirstOrDefault();

            Console.WriteLine("Best: " + bestProduct.Name);




            var zeroSales = products.GroupJoin(sales, p => p.Id, s => s.ProductId, (p, s) => new { p, s }).Where(x => !x.s.Any()).Select(x => x.p);

            foreach (var p in zeroSales)
            {
                Console.WriteLine(p.Name + " No Sales");
            }

            var booksAfter2015 = books.Where(b => b.Year > 2015);

            foreach (var b in booksAfter2015)
            {
                Console.WriteLine(b.Title);
            }


            var genreCount = books.GroupBy(b => b.Genre).Select(g => new
            {
                Genre = g.Key,
                Count = g.Count()
            });

            foreach (var g in genreCount)
            {
                Console.WriteLine(g.Genre + " " + g.Count);
            }


            var mostExpensiveBooksPerGenre = books.GroupBy(b => b.Genre).Select(g => g.OrderByDescending(b => b.Price).First());

            foreach (var b in mostExpensiveBooksPerGenre)
            {
                Console.WriteLine(b.Genre + " " + b.Title);
            }


            var authors = books.Select(b => b.Author).Distinct();

            foreach (var a in authors)
            {
                Console.WriteLine(a);
            }


            var totalOrderPerCustomerAmount = customers.GroupJoin(orders, c => c.Id, o => o.CustomerId, (c, o) => new
            {
                c.Name,
                Total = o.Sum(x => x.Amount)
            });

            foreach (var t in totalOrderPerCustomerAmount)
            {
                Console.WriteLine(t.Name + " " + t.Total);
            }

            var noOrders = customers.GroupJoin(orders, c => c.Id, o => o.CustomerId, (c, o) => new { c, o }).Where(x => !x.o.Any()).Select(x => x.c);

            foreach (var c in noOrders)
            {
                Console.WriteLine(c.Name + " No Orders");
            }


            var spentAbove50000 = totalOrderPerCustomerAmount.Where(x => x.Total > 50000);

            foreach (var b in spentAbove50000)
            {
                Console.WriteLine(b.Name);
            }

            var sortedCustomers = totalOrderPerCustomerAmount.OrderByDescending(x => x.Total);

            foreach (var s in sortedCustomers)
            {
                Console.WriteLine(s.Name + " " + s.Total);
            }



            var ratingAbove8 = movies.Where(m => m.Rating > 8);

            foreach (var m in ratingAbove8)
            {
                Console.WriteLine(m.Title + " " + m.Rating);
            }



            var avgByGenre = movies.GroupBy(m => m.Genre).Select(g => new
            {
                Genre = g.Key,
                AvgRating = g.Average(m => m.Rating)
            });

            foreach (var g in avgByGenre)
            {
                Console.WriteLine(g.Genre + " " + g.AvgRating);
            }



            var latestMovie = movies.GroupBy(m => m.Genre).Select(g => g.OrderByDescending(m => m.Year).First());
            foreach (var m in latestMovie)
            {
                Console.WriteLine(m.Genre + " " + m.Title + " " + m.Year);
            }


            var top5Movies = movies.OrderByDescending(m => m.Rating).Take(5);

            foreach (var m in top5Movies)
            {
                Console.WriteLine(m.Title + " " + m.Rating);
            }



            var balance = transactions.GroupBy(t => t.Acc).Select(g => new
            {
                Acc = g.Key,
                Balance = g.Sum(t =>
                t.Type == "Credit" ? t.Amount : -t.Amount)
            });

            foreach (var b in balance)
            {
                Console.WriteLine("Account: " + b.Acc + " Balance: " + b.Balance);
            }

            var suspicious = transactions.GroupBy(t => t.Acc).Where(g => g.Where(t => t.Type == "Debit").Sum(x => x.Amount) > g.Where(t => t.Type == "Credit").Sum(x => x.Amount)).Select(g => g.Key);

            foreach (var s in suspicious)
            {
                Console.WriteLine("Suspicious Account: " + s);
            }




            var highestTxn = transactions.GroupBy(t => t.Acc).Select(g => new
            {
                Acc = g.Key,
                MaxAmount = g.Max(t => t.Amount)
            });

            foreach (var h in highestTxn)
            {
                Console.WriteLine("Account: " + h.Acc + " Highest Txn: " + h.MaxAmount);
            }


            var totalCart = cart.Sum(c => c.Price * c.Qty);
            Console.WriteLine("Total Cart Value: " + totalCart);


            var categoryTotal = cart.GroupBy(c => c.Category).Select(g => new
            {
                Category = g.Key,
                Total = g.Sum(c => c.Price * c.Qty)
            });

            foreach (var c in categoryTotal)
            {
                Console.WriteLine(c.Category + " " + c.Total);
            }



            var discountCart = cart.Select(c => new
            {
                c.Name,
                FinalPrice = c.Category == "Electronics" ? c.Price * 0.9 : c.Price
            });

            foreach (var item in discountCart)
            {
                Console.WriteLine(item.Name + " " + item.FinalPrice);
            }



            var topUsers = users.GroupJoin(posts, u => u.Id, p => p.UserId, (u, p) => new
            {
                u.Name,
                TotalLikes = p.Sum(x => x.Likes)
            }).OrderByDescending(x => x.TotalLikes);

            foreach (var u in topUsers)
            {
                Console.WriteLine(u.Name + " Likes: " + u.TotalLikes);
            }


            var usersByCountry = users.GroupBy(u => u.Country);

            foreach (var group in usersByCountry)
            {
                Console.WriteLine("Country: " + group.Key);
                foreach (var user in group)
                {
                    Console.WriteLine("  " + user.Name);
                }
            }


            var inactiveUsers = users.GroupJoin(posts, u => u.Id, p => p.UserId, (u, p) => new { u, p }).Where(x => !x.p.Any()).Select(x => x.u);

            foreach (var u in inactiveUsers)
            {
                Console.WriteLine(u.Name);
            }


            var avgLikes = posts.Any() ? posts.Average(p => p.Likes) : 0;
            Console.WriteLine("Average Likes: " + avgLikes);


        }
    }
}
