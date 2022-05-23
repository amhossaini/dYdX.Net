from starkex.helpers import private_key_to_public_key_pair_hex
import dateutil.parser as dp
import sys

key_pair = private_key_to_public_key_pair_hex(sys.argv[1])

print(key_pair[0]+'&'+key_pair[1])
