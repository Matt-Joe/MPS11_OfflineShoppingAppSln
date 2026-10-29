Why OrderItem stores its own price instead of reading the product’s current price.

OrderItem stores its own price because it captures the price of the product at the time the purchase was made. 
At checkout there may be a discount applied which won't match the price of the product. The price of the product may also
change in the future and this can create inconsistencies in the database.
