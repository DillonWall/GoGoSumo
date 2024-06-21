CREATE TABLE roles (
    role_id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL
);

CREATE TABLE users (
    clerk_id VARCHAR(32) PRIMARY KEY,
    phone VARCHAR(20) NOT NULL,
    fluent_languages VARCHAR(5)[],
	role_id INT REFERENCES roles(role_id)
);

INSERT INTO roles (role_name) VALUES
    ('Customer'),
    ('Guide'),
    ('WeddingPlanner');

CREATE TABLE events (
	event_id SERIAL PRIMARY KEY,
	event_name VARCHAR(100),
	event_date DATE,
	event_location VARCHAR(255),
	event_gogo_price_yen DECIMAL(13, 2)
);

CREATE TABLE weddings (
    wedding_id SERIAL PRIMARY KEY,
    event_id INT REFERENCES events(event_id) ON DELETE CASCADE,
    wedding_bride_name VARCHAR(100),
    wedding_groom_name VARCHAR(100),
    wedding_budget_yen DECIMAL(13, 2),
	wedding_planner_id VARCHAR(32)
);

CREATE TABLE tours (
	tour_id SERIAL PRIMARY KEY,
	event_id INT REFERENCES events(event_id) ON DELETE CASCADE,
	tour_guide_id VARCHAR(32)
);

CREATE TABLE tour_customers (
	tour_id INT REFERENCES tours(tour_id),
	customer_id VARCHAR(32) REFERENCES users(clerk_id)
);


INSERT INTO users (clerk_id, phone, fluent_languages, role_id)
VALUES
    ('user1', '123-456-7890', ARRAY['EN'], 1),
    ('user2', '987-654-3210', ARRAY['JP'], 2),
    ('user3', '555-123-4567', ARRAY['CN_HK'], 3);

INSERT INTO events (event_name, event_date, event_location, event_gogo_price_yen)
VALUES
    ('Conference A', '2024-06-01', 'Venue X', 1500.00),
    ('Meeting B', '2024-06-15', 'Venue Y', 800.50),
    ('Social Gathering C', '2024-07-10', 'Venue Z', 300.75);

INSERT INTO weddings (event_id, wedding_bride_name, wedding_groom_name, wedding_budget_yen, wedding_planner_id)
VALUES
    (1, 'Alice', 'Bob', 20000.00, 'planner1'),
    (2, 'Claire', 'David', 15000.00, 'planner2'),
    (3, 'Eva', 'Frank', 18000.00, 'planner3');

INSERT INTO tours (event_id, tour_guide_id)
VALUES
    (1, 'user1'),
    (2, 'user2'),
    (3, 'user3');

INSERT INTO tour_customers (tour_id, customer_id)
VALUES
    (1, 'user1'),
    (2, 'user2'),
    (3, 'user3');
