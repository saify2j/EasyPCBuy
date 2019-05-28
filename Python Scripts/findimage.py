import requests
from bs4 import BeautifulSoup

page = requests.get("https://ryanscomputers.com/intel-pentium-gold-g5400-3-7ghz-4mb-cache-lga1151-socket-processor.html")
soup = BeautifulSoup(page.content, 'html.parser')
# for a in soup.find_all('a', attrs={"class": "product-image"}):
#     if a.img:
#         print(a.img['src'])
list_a = soup.find_all('div', attrs={"class":"std"})
temp =str(list_a[1].get_text())
temp = temp.strip()
temp = temp.replace(",","\n")
print(temp)
