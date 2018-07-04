# ConsoleApppRequests

This is a simple  Console Appp on C# which makes HTTP request on server and retrieves some JSON data and then deserializes it to use linq requests.

## To successfully do it you will need:

#### 1) Create a C# Console Application

#### 2) Use HttpClient (or WebClient) to receive a set of the open data by means of API of requests to https://5b128555d50a5c0014ef1204.mockapi.io/:endpoint

Where _endpoint_ can have the following values:
  > users

  > posts

  > comments

  > todos

address
#### 3) Present the received data as a set of entities (nested objects).
 > -Users
 
 > --- Posts
 
 > ------- Comments
 
 > --- Todos
 
#### 4) To deserialize, use [Newtonsoft](https://www.newtonsoft.com/json).

#### 5) Entities must be connected. 

In order to determine the relationship, you must use id. To create a hierarchy, use Join () from Linq. - implement a set of methods for retrieving data from the collection (or several collections, depending on the query)

#### 6) List of requests:

1. Get the number of comments under the posts of a particular user (on aidi) (list from post-number)

2. Get a list of comments under the posts of a particular user (on aide), where body comment <50 characters (list of comments)

3. Get the list (id, name) from the list of todos that are executed for a specific user (by IDE)

4. Get a list of users in alphabetical order (ascending) with sorted todo items by length name (descending)

5. Get the following structure (pass User Id to parameters)

    * User

    * Last post by user (by date)

    * Number of comments under the last post

    * Number of unfulfilled tasks for the user

    * The most popular user post (where most of the comments with a text length of more than 80 characters)

    * The most popular user post (where most of the likes)

6. Get the following structure (pass the Id post to the parameters)

    * Post

    * The longest comment of the post

    * The most lukewarm comment on the post
  
    * Number of comments under the post where or 0 likes or text length <80
    
### Attention
*Each sample must be performed in one method.*  
    
### Have fun :)
_“Quality is much better than quantity. One home run is much better than two doubles.”
-Steve Jobs_
