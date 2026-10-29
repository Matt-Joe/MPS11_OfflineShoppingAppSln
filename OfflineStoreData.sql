-- ============================================
-- OFFLINE STORE: FORMULA 1 CAR UPGRADES
-- Populate Category and Product tables
-- Prices are stored in cents (e.g. R1,500.00 = 150000)
-- ============================================

-- ============================================
-- CUSTOMERS: FORMULA 1 RACERS
-- ============================================

INSERT INTO Customer (Name, Surname) VALUES
('Max', 'Verstappen'),
('Lewis', 'Hamilton'),
('Charles', 'Leclerc'),
('Lando', 'Norris'),
('Oscar', 'Piastri'),
('George', 'Russell'),
('Fernando', 'Alonso'),
('Carlos', 'Sainz'),
('Sergio', 'Perez'),
('Pierre', 'Gasly'),
('Esteban', 'Ocon'),
('Alexander', 'Albon'),
('Lance', 'Stroll'),
('Yuki', 'Tsunoda'),
('Nico', 'Hulkenberg'),
('Oliver', 'Bearman'),
('Liam', 'Lawson'),
('Franco', 'Colapinto'),
('Gabriel', 'Bortoleto'),
('Kimi', 'Antonelli');

-- 1. CATEGORIES
INSERT INTO Category (Name) VALUES
('Aerodynamics'),
('Engine'),
('Power Unit (PU)'),
('Suspension'),
('Braking System'),
('Transmission'),
('Tyres'),
('Electronics'),
('Cooling System'),
('Weight Reduction');

-- 2. PRODUCTS: AERODYNAMICS
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(1, 'Front Wing Upgrade', 4500000, 5),
(1, 'Rear Wing Assembly', 3800000, 4),
(1, 'Floor and Diffuser Kit', 6200000, 3),
(1, 'Sidepod Aerodynamic Kit', 2900000, 6),
(1, 'DRS Mechanism Upgrade', 1800000, 4);

-- 3. PRODUCTS: ENGINE
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(2, 'High Performance V6 Engine', 85000000, 2),
(2, 'Combustion Chamber Upgrade', 12500000, 3),
(2, 'Turbocharger Assembly', 22000000, 2),
(2, 'Fuel Injection System', 8500000, 5),
(2, 'High Flow Air Intake', 3200000, 7);

-- 4. PRODUCTS: POWER UNIT (PU)
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(3, 'MGU-K Recovery Unit', 28000000, 2),
(3, 'Energy Store Battery Pack', 19500000, 3),
(3, 'Control Electronics Module', 7200000, 4),
(3, 'Hybrid Power Unit Upgrade', 45000000, 2),
(3, 'Energy Recovery System', 16000000, 3);

-- 5. PRODUCTS: SUSPENSION
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(4, 'Front Push-Rod Suspension', 9500000, 4),
(4, 'Rear Suspension Assembly', 11000000, 3),
(4, 'Adjustable Damper Set', 4200000, 6),
(4, 'Carbon Fibre Wishbone Set', 5800000, 4);

-- 6. PRODUCTS: BRAKING SYSTEM
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(5, 'Carbon Ceramic Brake Discs', 6800000, 4),
(5, 'High Performance Brake Calipers', 3500000, 5),
(5, 'Brake Duct Cooling Kit', 2100000, 6),
(5, 'Brake-by-Wire Control Unit', 4900000, 3);

-- 7. PRODUCTS: TRANSMISSION
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(6, 'Eight-Speed Gearbox Assembly', 32000000, 2),
(6, 'Carbon Fibre Clutch Kit', 5600000, 4),
(6, 'Differential Control Upgrade', 7800000, 3),
(6, 'Seamless Shift Mechanism', 4200000, 5);

-- 8. PRODUCTS: TYRES
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(7, 'Soft Compound Tyre Set', 1800000, 10),
(7, 'Medium Compound Tyre Set', 1600000, 10),
(7, 'Hard Compound Tyre Set', 1450000, 10),
(7, 'Intermediate Tyre Set', 1900000, 6),
(7, 'Wet Weather Tyre Set', 2100000, 5);

-- 9. PRODUCTS: ELECTRONICS
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(8, 'Race Control ECU', 9200000, 3),
(8, 'Telemetry Sensor Package', 3800000, 5),
(8, 'Steering Wheel Control Module', 6500000, 3),
(8, 'Data Acquisition System', 5100000, 4);

-- 10. PRODUCTS: COOLING SYSTEM
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(9, 'Radiator Assembly', 4200000, 4),
(9, 'Oil Cooling System', 3100000, 5),
(9, 'High Efficiency Heat Exchanger', 2600000, 5),
(9, 'Advanced Cooling Ducts', 1700000, 7);

-- 11. PRODUCTS: WEIGHT REDUCTION
INSERT INTO Product (CategoryId, Name, Price, Stock) VALUES
(10, 'Carbon Fibre Body Panels', 12500000, 3),
(10, 'Lightweight Wheel Set', 7200000, 4),
(10, 'Titanium Fastener Kit', 950000, 8),
(10, 'Lightweight Composite Seat', 2800000, 5);
