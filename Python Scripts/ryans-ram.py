from bs4 import BeautifulSoup
import requests
import csv
import os
from time import sleep
import pandas as pd


def get_last_page(url, component):
    main_page = requests.get(url + component+".html")
    soup = BeautifulSoup(main_page.content, 'html.parser')
    # print(soup.prettify())
    number_of_pages = []
    for a in soup.select('.pages li a'):
        number_of_pages.append(a.get_text())

    last_page = int(number_of_pages[-2])
    return last_page


def load_data(url, component, last_page):
    wr = open('Ryans-Final\\ryans-' + component + '.csv', 'a', newline='', encoding="utf-8")
    writer = csv.writer(wr)
    header = ['product_name', 'model', 'link', 'ryans_price', 'brand', 'picLink', 'capacity', 'description']
    writer.writerow(header)
    for i in range(1, last_page + 1):
        number = i
        page = requests.get(url + component + ".html?p=" + str(number))
        soup = BeautifulSoup(page.content, 'html.parser')
        names = []
        links = []
        prices = []
        models = []
        capacity = []
        descriptions = []
        brands = []
        pic_links =[]

        for a in soup.select('.container h2 a'):
            links.append(a['href'])
            model_name, brand_name, description, cap = load_product_model(a['href'])
            #print(cap)
            print(brand_name)
            brands.append(brand_name)
            names.append(a.get_text(strip=True))
            models.append(model_name)
            descriptions.append(description)
            capacity.append(cap)
        price_list = soup.find_all('p', attrs={"class": "special-price"})
        for a in soup.find_all('a', attrs={"class": "product-image"}):
            if a.img:
                print(a.img['src'])
                pic_links.append(a.img['src'])

        for j in price_list:
            prices.append(j.get_text()[18:])
        rows = zip(names, models, links, prices, brands, pic_links,capacity,descriptions)


        for row in rows:
                writer.writerow(row)
    print("Writing to file done...")


def load_product_model(link):
    model = "No Model Found"
    brand = "No Brand Specified"
    description = "No Description Found"
    capacity ="Capacity not specified"
    try:
        page = requests.get(link)
        soup = BeautifulSoup(page.content, 'html.parser')
        model = soup.find('table', class_='data-table').find('th', text='Model').find_next_sibling('td').text
        brand = soup.find('table', class_='data-table').find('th', text='Brand').find_next_sibling('td').text
        capacity = soup.find('table', class_='data-table').find('th', text='Capacity').find_next_sibling('td').text
        list_a = soup.find_all('div', attrs={"class": "std"})
        temp = str(list_a[1].get_text())
        temp = temp.strip()
        temp = temp.replace(",", "\n")
        description = temp
        # print(temp)

    except:
        pass
    return model, brand, description,capacity


def main():
    url = "https://ryanscomputers.com/components/"
    # components = ["processor",
    #              "desktop-ram",
    #              "mainboard",
    #              "graphics-card"]
    components = ["desktop-ram"]
    for component in components:
        last_page = get_last_page(url, component)
        load_data(url, component, last_page)


if __name__ == '__main__':
    main()
