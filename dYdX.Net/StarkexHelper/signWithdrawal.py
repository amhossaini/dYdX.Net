from starkex.withdrawal import SignableWithdrawal
import dateutil.parser as dp
import sys

withdrawal_to_sign = SignableWithdrawal(
                network_id=int(sys.argv[1]),
                position_id=int(sys.argv[2]),
                client_id=sys.argv[3],
                human_amount=sys.argv[4],
                expiration_epoch_seconds=dp.parse(sys.argv[5]).timestamp(),
            )

withdrawal_signature = withdrawal_to_sign.sign(sys.argv[6])
print(withdrawal_signature)