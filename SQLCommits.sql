CREATE TABLE customers
(
  customer_id SERIAL PRIMARY KEY,
  name VARCHAR(20) NOT NULL
);

CREATE TABLE orders
(
  order_id SERIAL PRIMARY KEY,
  customer_id INTEGER REFERENCES Customers (customer_id)
);

SELECT name FROM customers WHERE name NOT IN 
(
  SELECT customers.name FORM customers
  JOIN orders ON orders.customer_id = customers.customer_id
  ORDER BY orders.order_id
);
