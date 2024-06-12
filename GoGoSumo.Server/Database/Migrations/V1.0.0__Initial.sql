CREATE TYPE fluent_language_type AS ENUM ('JP', 'EN', 'CN-HK', 'CN-ZH');

CREATE TYPE user_role_type AS ENUM ('Customer', 'Guide', 'Wedding Planner');

CREATE TABLE users (
    id serial PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100),
    phone VARCHAR(20),
    password VARCHAR(100),
    fluent_languages fluent_language_type[],
    role user_role_type
);
