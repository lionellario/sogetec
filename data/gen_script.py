from io import TextIOWrapper
import json
from uuid import uuid7
from slugify import slugify

CAT_NAME_TO_ID: dict[str, str] = {}


def _generate_categories(outf: TextIOWrapper):
    with open('categories.json', 'r') as f:
        categories = json.load(f)
    
    outf.write("START TRANSACTION;\n\n")

    outf.write("SELECT ('Creating Categories...') AS Message;\n\n")

    outf.write("INSERT INTO category (\n")
    outf.write("\tid,\n")
    outf.write("\tname,\n")
    outf.write("\tslug,\n")
    outf.write("\tparent_id,\n")
    outf.write("\tsort_order\n")
    outf.write(") VALUES\n")

    sort_order = 1
    for category in categories:
        id = uuid7()
        name = category["name"]
        CAT_NAME_TO_ID[name] = id
        slug = slugify(name)
        parent_id = CAT_NAME_TO_ID.get(category["parent"])
        parent_id = f"'{parent_id}'" if parent_id else "NULL"
        outf.write("(\n")
        outf.write(f"\t'{id}',\n")
        outf.write(f"\t'{name}',\n")
        outf.write(f"\t'{slug}',\n")
        outf.write(f"\t{parent_id},\n")
        outf.write(f"\t{sort_order}\n")
        outf.write("),\n")
        sort_order += 1
    outf.seek(outf.tell() - 2)
    outf.write(";\n\n")

    outf.write("COMMIT;\n")

def run():
    with open("script.sql", "w") as f:
        _generate_categories(f)

run()