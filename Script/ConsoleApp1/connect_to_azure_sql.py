import pyodbc

# Configuration for Azure SQL Database
server = 'ajtracker-server.database.windows.net'  # Your server name
database = 'AJTrackerdb'  # Your database name
username = 'adminuser'  # Your SQL admin username
password = 'MYPASSWORD'  # Your SQL password
driver = '{ODBC Driver 18 for SQL Server}'

conn = None  # Initialize conn to None

def add_movie_adaptation(title, original_source, review):
    """Add a new movie adaptation to the database."""
    try:
        cursor = conn.cursor()
        cursor.execute(
            "INSERT INTO MovieAdaptations (Title, OriginalSource, Review) VALUES (?, ?, ?)",
            (title, original_source, review)
        )
        conn.commit()
        print(f"New movie adaptation '{title}' added.")
    except Exception as e:
        print(f"Error adding movie adaptation: {e}")

def update_movie_adaptation(adaptation_id, new_title, new_review):
    """Update an existing movie adaptation in the database."""
    try:
        cursor = conn.cursor()
        cursor.execute(
            "UPDATE MovieAdaptations SET Title = ?, Review = ? WHERE Id = ?",
            (new_title, new_review, adaptation_id)
        )
        conn.commit()
        print(f"Movie adaptation ID {adaptation_id} updated to '{new_title}'.")
    except Exception as e:
        print(f"Error updating movie adaptation: {e}")

def main():
    global conn
    # Establish a connection
    try:
        conn = pyodbc.connect('DRIVER=' + driver + ';SERVER=' + server + ',1433;DATABASE=' + database + ';UID=' + username + ';PWD=' + password)
        print("Connection successful.\n")

        while True:
            print("Choose an option:")
            print("1. Add Movie Adaptation")
            print("2. Update Movie Adaptation")
            print("3. View Movie Adaptations")
            print("4. Exit")

            option = input("Enter your choice (1-4): ")

            if option == '1':
                title = input("Enter movie title: ")
                original_source = input("Enter original source: ")
                review = input("Enter your review: ")
                add_movie_adaptation(title, original_source, review)

            elif option == '2':
                adaptation_id = int(input("Enter the ID of the adaptation to update: "))
                new_title = input("Enter new movie title: ")
                new_review = input("Enter new review: ")
                update_movie_adaptation(adaptation_id, new_title, new_review)

            elif option == '3':
                cursor = conn.cursor()
                cursor.execute("SELECT * FROM MovieAdaptations")
                rows = cursor.fetchall()
                print("\nCurrent Movie Adaptations:")
                for row in rows:
                    print(f"ID: {row.Id}, Title: {row.Title}, Original Source: {row.OriginalSource}, Review: {row.Review}")
                print()  # Add a new line for better readability
            
            elif option == '4':
                print("Exiting the application.")
                break

            else:
                print("Invalid option. Please try again.")

    except Exception as e:
        print(f"Error: {e}")
    finally:
        # Close the connection
        if conn:
            conn.close()

if __name__ == "__main__":
    main()
