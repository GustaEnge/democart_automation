Feature: CartTestApp
	Browse the website Demo Store and finish a purchase order


@CartTestAppSteps
Scenario: Buy a product on Demo Store
	Given I am on the Demo Store page
	When I search for <product>
	When The page loads the searched product: <product>
	When I click on the chosen product and add it to the cart
	And I check this <product> on cart as well as total price
	When I do the checkout, fill in the <country>, <state>,<city>,<address>,<cep>,<email>,<name> and select Phone Order Payment by passing <phone>
	Then proceed with the order.



Examples: related to product
| product     | address            | cep      | email               | city         | state     | country     | name     | phone		    |
| laptop      | Rua Sebastião 46   | 15115000 | teste@gmails.com    | Bady Bassitt | São Paulo | Brazil      | Gustavo  |  5561988078998|
| notebook    | Albert Street 56   | 4242443  | cunha.goc@gmail.com | Calgary      | Alberta   | Canada      | Caio     |14035873168664	|
| playstation | Segetzstrasse 4500 | 242323   | cunha.goc@gmail.com | Solothurn    | Soleure   | Switzerland | André    |	41326256520	|
#negativo busca| casaco        | 5561988078998|
#negativo produto esgotado (backorder)| playstation   | 5561988078998|

#Examples: related to checkout
#| address            | cep      | email               | city         | state     | country     | phone				|
#| Rua Sebastião 46   | 15115000 | teste@gmails.com    | Bady Bassitt | São Paulo | Brazil      |  5561988078998     |
#| Albert Street 56   | 4242443  | cunha.goc@gmail.com | Calgary      | Alberta   | Canada      |  14035873168664    |
#| Segetzstrasse 4500 | 242323   | cunha.goc@gmail.com | Solothurn    | Soleure   | Switzerland |  41326256520		|

#Testar quando houver mais de um produto por sessão/tab num mesmo teste, somando o total
#Testar quando tiver apenas um produto pesquisado, ao usar o random
#Testar se o email está com máscara certa