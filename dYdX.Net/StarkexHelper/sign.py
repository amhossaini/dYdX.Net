

from starkex.order import SignableOrder
from datetime import datetime
import dateutil.parser as dp

import sys
import json


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