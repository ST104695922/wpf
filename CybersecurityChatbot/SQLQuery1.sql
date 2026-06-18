-- run this in SQL Server or localdb before launching the app
 
CREATE DATABASE prog_tasks;
USE prog_tasks;

 
CREATE TABLE demo_tasks (
    task_id   INT PRIMARY KEY IDENTITY(1,1),
    task_name VARCHAR(100),
    task_description VARCHAR(100),
    task_dueDate VARCHAR(20),
    task_status VARCHAR(20)
);

-- check
--SELECT * FROM demo_tasks;