

from starkex.order import SignableOrder
from datetime import datetime
import dateutil.parser as dp

import sys
import json

# print('Number of arguments:', len(sys.argv), 'arguments.')
# print('Argument List:', str(sys.argv))

# obj = sys.argv[1]
# print(obj)
# obj = '{"side" : "BUY"}'
# y = json.loads(obj)
# print(y["side"])


# d = datetime.now()

# print(d.strftime(
#         '%Y-%m-%dT%H:%M:%S.%f',
#     )[:-3] + 'Z')

# ts = dp.parse(d.strftime(
#         '%Y-%m-%dT%H:%M:%S.%f',
#     )[:-3] + 'Z').timestamp()
# print(ts)

# order_to_sign = SignableOrder(
#                 network_id=1,
#                 position_id='2323',
#                 client_id='987676',
#                 market='BTC-USD',
#                 side='BUY',
#                 human_size='1.1',
#                 human_price='43000',
#                 limit_fee='100000',
#                 expiration_epoch_seconds=1548664452.999,
#             )
# print(dp.parse(sys.argv[9]).timestamp())
order_to_sign = SignableOrder(
                network_id=int(sys.argv[1]),
                position_id=sys.argv[2],
                client_id=sys.argv[3],
                market=sys.argv[4],
                side=sys.argv[5],
                human_size=sys.argv[6],
                human_price=sys.argv[7],
                limit_fee=sys.argv[8],
                expiration_epoch_seconds=dp.parse(sys.argv[9]).timestamp(),
            )
order_signature = order_to_sign.sign(sys.argv[10])
print(order_signature)