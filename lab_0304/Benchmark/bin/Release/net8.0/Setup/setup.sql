CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    role INT NOT NULL,
    name TEXT
);


INSERT INTO users(login, password, role, name)
VALUES 
    ('user1', 'password1', 'admin', 'Alice'),
    ('user2', 'password2', 'user', 'Bob'),
    ('user3', 'password3', 'user', 'Charlie');