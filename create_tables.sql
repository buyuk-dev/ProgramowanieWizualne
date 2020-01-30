create table Makers(
	name text not null,
	number text not null,
	address text not null
);

create table Violins(
	name text not null,
	maker text not null,
	year integer not null,
	price integer not null,
	state string not null
);

insert into Violins (name, maker, year, price, state)
values
("The Molitor", "Stradivari", 1697, 2700000, "good"),
("Julia", "Henglewski", 1995, 15000, "good"),
("The Dolphin", "Stradivari", 1714, 4000000, "average"),
("The Lord Wilton", "Guarneri del Gesu", 1742, 4300000, "bad");

insert into Makers (name, number, address)
values
("Stradivari", "123456789", "Cremona"),
("Guarneri", "987654321", "Cremona"),
("Henglewski", "555555555", "Poznań"); 



