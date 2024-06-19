CREATE TABLE users (
    clerk_id VARCHAR(32) PRIMARY KEY,
    phone VARCHAR(20) NOT NULL,
    fluent_languages VARCHAR(5)[] NOT NULL,
    role VARCHAR(100) NOT NULL
);