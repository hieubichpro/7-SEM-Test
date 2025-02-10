drop table if exists users;
drop table if exists clubs;
drop table if exists leagues;
drop table if exists matches;
create table if not exists Users
(
	id serial primary key,
	login varchar(64) not null unique,
	password varchar(64) not null,
	role varchar(64) not null,
	name varchar(64) default 'hieu'
);

create table if not exists Clubs
(
	id serial not null primary key,
	name varchar(64) not null unique,
	id_league int default -1
);

create table if not exists Leagues
(
	id serial not null primary key,
	name varchar(64) not null unique,
	id_user int,
	
	foreign key (id_user) references Users(id) on delete cascade
);

create table if not exists Matches
(
	id serial not null primary key,
	goal_home_club int default -1,
	goal_guest_club int default -1,
	id_league int not null,
	id_home_club int not null,
	id_guest_club int not null,
	
	foreign key (id_league) references Leagues(id) on delete cascade,
	foreign key (id_home_club) references Clubs(id) on delete cascade,
	foreign key (id_guest_club) references Clubs(id) on delete cascade
);

INSERT INTO Users (login, password, role, name)
VALUES 
    ('user1', 'password1', 'admin', 'Alice'),
    ('user2', 'password2', 'user', 'Bob'),
    ('user3', 'password3', 'user', 'Charlie');

INSERT INTO Clubs (name, id_league)
VALUES 
    ('Club A', 1),
    ('Club B', 1),
    ('Club C', 2);

INSERT INTO Leagues (name, id_user)
VALUES 
    ('League 1', 1),
    ('League 2', 2),
    ('League 3', 3);

INSERT INTO Matches (goal_home_club, goal_guest_club, id_league, id_home_club, id_guest_club)
VALUES 
    (2, 1, 1, 1, 2),
    (3, 3, 1, 2, 3),
    (0, 2, 2, 1, 3);