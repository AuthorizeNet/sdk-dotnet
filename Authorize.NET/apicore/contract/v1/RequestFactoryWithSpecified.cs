namespace authorizenet.apicore.contract.v1
{
    using System;
    using authorizenet.apicore.contract.v1;

#pragma warning disable 169
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
    public static class RequestFactoryWithSpecified
    {

        public static void messagesType ( messagesType request) { }

        public static void messagesTypeMessage ( messagesTypeMessage request) { }

        public static void paymentSimpleType(paymentSimpleType request)
        {
            if (null != request)
            {
                var type = request.Item as bankAccountType;
                if (type != null) { bankAccountType(type); }

                var item = request.Item as creditCardSimpleType;
                if (item != null) { creditCardSimpleType(item); }
            }
        }

        public static void bankAccountType(bankAccountType request)
        {
            if (null != request)
            {
                if (0 <= (int)request.accountType) { request.accountTypeSpecified = true; }
                if (0 <= (int)request.echeckType) { request.echeckTypeSpecified = true; }
            }
        }

        public static void creditCardSimpleType ( creditCardSimpleType request) { }

        public static void creditCardType ( creditCardType request) { }

        public static void searchCriteriaCustomerProfileType ( searchCriteriaCustomerProfileType request) { }

        public static void customerProfileSummaryType ( customerProfileSummaryType request) { }
    
        public static void subscriptionDetail ( subscriptionDetail request) { }
    
        public static void paging ( paging request) { }
    
        public static void ARBGetSubscriptionListSorting ( ARBGetSubscriptionListSorting request) { }
    
        public static void permissionType ( permissionType request) { }
    
        public static void merchantContactType ( merchantContactType request) { }

        public static void mobileDeviceType(mobileDeviceType request)
        {
            if (0 <= (int) request.deviceActivation) { request.deviceActivationSpecified = true; }
        }

        public static void transactionSummaryType(transactionSummaryType request)
        {
            if (request.hasReturnedItems) { request.hasReturnedItemsSpecified = true; }
        }

        public static void subscriptionPaymentType ( subscriptionPaymentType request) { }
    
        public static void ArrayOfSetting ( ArrayOfSetting request) { }
    
        public static void settingType ( settingType request) { }
    
        public static void emailSettingsType ( emailSettingsType request) { }
    
        public static void transRetailInfoType ( transRetailInfoType request) { }
    
        public static void ccAuthenticationType ( ccAuthenticationType request) { }
    
        public static void paymentProfile ( paymentProfile request) { }
    
        public static void customerProfilePaymentType ( customerProfilePaymentType request) { }

        public static void transactionRequestType(transactionRequestType request)
        {
            if (null != request)
            {
                if (0 <= request.amount) { request.amountSpecified = true; }
                if (request.taxExempt) { request.taxExemptSpecified = true; }
            }
        }

        public static void paymentType ( paymentType request) { }
    
        public static void encryptedTrackDataType ( encryptedTrackDataType request) { }
    
        public static void KeyBlock ( KeyBlock request) { }
    
        public static void KeyValue ( KeyValue request) { }
    
        public static void KeyManagementScheme ( KeyManagementScheme request) { }
    
        public static void KeyManagementSchemeDUKPT ( KeyManagementSchemeDUKPT request) { }
    
        public static void KeyManagementSchemeDUKPTMode ( KeyManagementSchemeDUKPTMode request) { }
    
        public static void KeyManagementSchemeDUKPTDeviceInfo ( KeyManagementSchemeDUKPTDeviceInfo request) { }
    
        public static void KeyManagementSchemeDUKPTEncryptedData ( KeyManagementSchemeDUKPTEncryptedData request) { }
    
        public static void payPalType ( payPalType request) { }
    
        public static void creditCardTrackType ( creditCardTrackType request) { }
    
        public static void orderType ( orderType request) { }
    
        public static void orderExType ( orderExType request) { }

        public static void lineItemType(lineItemType request)
        {
            if (request.taxable) { request.taxableSpecified = true; }
        }

        public static void extendedAmountType ( extendedAmountType request) { }

        public static void customerDataType(customerDataType request)
        {
            if (0 <= (int) request.type) { request.typeSpecified = true; }
        }

        public static void driversLicenseType ( driversLicenseType request) { }
    
        public static void customerAddressType ( customerAddressType request) { }
    
        public static void nameAndAddressType ( nameAndAddressType request) { }
    
        public static void customerAddressExType ( customerAddressExType request) { }
    
        public static void userField ( userField request) { }
    
        public static void solutionType ( solutionType request) { }
    
        public static void returnedItemType ( returnedItemType request) { }
    
        public static void batchStatisticType ( batchStatisticType request) {
            if (0 <= request.returnedItemAmount) { request.returnedItemAmountSpecified = true; }
            if (0 <= request.returnedItemCount) { request.returnedItemCountSpecified = true; }
            if (0 <= request.chargebackAmount) { request.chargebackAmountSpecified = true; }
            if (0 <= request.chargebackCount) { request.chargebackCountSpecified = true; }
            if (0 <= request.correctionNoticeCount) { request.correctionNoticeCountSpecified = true; }
            if (0 <= request.chargeChargeBackAmount) { request.chargeChargeBackAmountSpecified = true; }
            if (0 <= request.chargeChargeBackCount) { request.chargeChargeBackCountSpecified = true; }
            if (0 <= request.refundChargeBackAmount) { request.refundChargeBackAmountSpecified = true; }
            if (0 <= request.refundChargeBackCount) { request.refundChargeBackCountSpecified = true; }
            if (0 <= request.chargeReturnedItemsAmount) { request.chargeReturnedItemsAmountSpecified = true; }
            if (0 <= request.chargeReturnedItemsCount) { request.chargeReturnedItemsCountSpecified = true; }
            if (0 <= request.refundReturnedItemsAmount) { request.refundReturnedItemsAmountSpecified = true; }
            if (0 <= request.refundReturnedItemsCount) { request.refundReturnedItemsCountSpecified = true; }
        }
    
        public static void batchDetailsType ( batchDetailsType request) { 
            if (DateTime.MinValue != request.settlementTimeUTC) { request.settlementTimeUTCSpecified = true; }
            if (DateTime.MinValue != request.settlementTimeLocal) { request.settlementTimeLocalSpecified = true; }
        }

        public static void FDSFilterType ( FDSFilterType request) { }
    
        public static void transactionDetailsType ( transactionDetailsType request) {
            if (0 <= request.requestedAmount) { request.requestedAmountSpecified = true; }
            if (0 <= request.prepaidBalanceRemaining) { request.prepaidBalanceRemainingSpecified = true; }
            if (request.taxExempt) { request.taxExemptSpecified = true; }
            if (request.recurringBilling) { request.recurringBillingSpecified = true; }
        }

        public static void paymentMaskedType ( paymentMaskedType request) { }
    
        public static void bankAccountMaskedType ( bankAccountMaskedType request) { 
            if (0 <= request.accountType) { request.accountTypeSpecified = true; }
            if (0 <= request.echeckType) { request.echeckTypeSpecified = true; }
        }
    
        public static void creditCardMaskedType ( creditCardMaskedType request) { }
    
        public static void profileTransVoidType ( profileTransVoidType request) { }
    
        public static void profileTransAmountType ( profileTransAmountType request) { }
    
        public static void profileTransRefundType ( profileTransRefundType request) { }
    
        public static void profileTransPriorAuthCaptureType ( profileTransPriorAuthCaptureType request) { }
    
        public static void profileTransOrderType ( profileTransOrderType request) { 
            if (request.taxExempt) { request.taxExemptSpecified = true; }
            if (request.recurringBilling) { request.recurringBillingSpecified = true; }
        }

        public static void profileTransCaptureOnlyType ( profileTransCaptureOnlyType request) { }
    
        public static void profileTransAuthOnlyType ( profileTransAuthOnlyType request) { }
    
        public static void profileTransAuthCaptureType ( profileTransAuthCaptureType request) { }
    
        public static void profileTransactionType ( profileTransactionType request) { }
    
        public static void driversLicenseMaskedType ( driversLicenseMaskedType request) { }
    
        public static void customerPaymentProfileBaseType ( customerPaymentProfileBaseType request) { 
            if (0 <= request.customerType) { request.customerTypeSpecified = true; }
        }
    
        public static void customerPaymentProfileMaskedType ( customerPaymentProfileMaskedType request) { }
    
        public static void customerPaymentProfileType ( customerPaymentProfileType request) { }
    
        public static void customerPaymentProfileExType ( customerPaymentProfileExType request) { }
    
        public static void customerProfileBaseType ( customerProfileBaseType request) { }
    
        public static void customerProfileExType ( customerProfileExType request) { }
    
        public static void customerProfileMaskedType ( customerProfileMaskedType request) { }
    
        public static void customerProfileType ( customerProfileType request) { }
    
        public static void customerType ( customerType request) { 
            if (0 <= request.type) { request.typeSpecified = true; }
        }    

        public static void paymentScheduleType ( paymentScheduleType request) {
            if ( null != request)
            {
                if (DateTime.MinValue != request.startDate) { request.startDateSpecified = true; }
                if (0 <= request.totalOccurrences) { request.totalOccurrencesSpecified = true; }
                if (0 <= request.trialOccurrences) { request.trialOccurrencesSpecified = true; }
            }
        }    

        public static void paymentScheduleTypeInterval ( paymentScheduleTypeInterval request) { }
    
        public static void ARBSubscriptionType ( ARBSubscriptionType request) {
            if (null != request)
            {
                if (0 <= request.amount) { request.amountSpecified = true; }
                if (0 <= request.trialAmount) { request.trialAmountSpecified = true; }
                paymentType(request.payment);
                paymentScheduleType(request.paymentSchedule);
            }
        }
    
        public static void impersonationAuthenticationType ( impersonationAuthenticationType request) { }
    
        public static void merchantAuthenticationType ( merchantAuthenticationType request) { }
    
        public static void ANetApiRequest ( ANetApiRequest request) { }
    
        public static void isAliveRequest ( isAliveRequest request) { }
    
        public static void authenticateTestRequest ( authenticateTestRequest request) { }
    
        public static void ARBCreateSubscriptionRequest ( ARBCreateSubscriptionRequest request) { }
    
        public static void ARBUpdateSubscriptionRequest ( ARBUpdateSubscriptionRequest request) { }
    
        public static void ARBCancelSubscriptionRequest ( ARBCancelSubscriptionRequest request) { }
    
        public static void ARBGetSubscriptionStatusRequest ( ARBGetSubscriptionStatusRequest request) { }
    
        public static void createCustomerProfileRequest ( createCustomerProfileRequest request) {
            if (0 <= (int) request.validationMode) { request.validationModeSpecified = true; }
        }
    
        public static void createCustomerPaymentProfileRequest ( createCustomerPaymentProfileRequest request) {
            if (0 <= (int) request.validationMode) { request.validationModeSpecified = true; }
        }
    
        public static void createCustomerShippingAddressRequest ( createCustomerShippingAddressRequest request) { }
    
        public static void getCustomerProfileRequest ( getCustomerProfileRequest request) { }
    
        public static void getCustomerPaymentProfileRequest ( getCustomerPaymentProfileRequest request) { }
    
        public static void getCustomerShippingAddressRequest ( getCustomerShippingAddressRequest request) { }
    
        public static void updateCustomerProfileRequest ( updateCustomerProfileRequest request) { }
    
        public static void updateCustomerPaymentProfileRequest ( updateCustomerPaymentProfileRequest request) {
            if (0 <= (int) request.validationMode) { request.validationModeSpecified = true; }
        }
    
        public static void updateCustomerShippingAddressRequest ( updateCustomerShippingAddressRequest request) { }
    
        public static void deleteCustomerProfileRequest ( deleteCustomerProfileRequest request) { }
    
        public static void deleteCustomerPaymentProfileRequest ( deleteCustomerPaymentProfileRequest request) { }
    
        public static void deleteCustomerShippingAddressRequest ( deleteCustomerShippingAddressRequest request) { }
    
        public static void createCustomerProfileTransactionRequest ( createCustomerProfileTransactionRequest request) { }
    
        public static void validateCustomerPaymentProfileRequest ( validateCustomerPaymentProfileRequest request) { }
    
        public static void getCustomerProfileIdsRequest ( getCustomerProfileIdsRequest request) { }
    
        public static void updateSplitTenderGroupRequest ( updateSplitTenderGroupRequest request) { }
    
        public static void getTransactionDetailsRequest ( getTransactionDetailsRequest request) { }
    
        public static void createTransactionRequest ( createTransactionRequest request) { }
    
        public static void getBatchStatisticsRequest ( getBatchStatisticsRequest request) { }

        public static void getSettledBatchListRequest(getSettledBatchListRequest request) {
            if (request.includeStatistics) { request.includeStatisticsSpecified = true; }
            if (DateTime.MinValue != request.firstSettlementDate) { request.firstSettlementDateSpecified = true; }
            if (DateTime.MinValue != request.lastSettlementDate) { request.lastSettlementDateSpecified = true; }
        }

        public static void getTransactionListRequest(getTransactionListRequest request) { }

        public static void getHostedProfilePageRequest(getHostedProfilePageRequest request) { }

        public static void getUnsettledTransactionListRequest(getUnsettledTransactionListRequest request) { }

        public static void mobileDeviceRegistrationRequest(mobileDeviceRegistrationRequest request)
        {
            if (null != request)
            {
                mobileDeviceType(request.mobileDevice);
            }
        }

        public static void mobileDeviceLoginRequest(mobileDeviceLoginRequest request) { }

        public static void logoutRequest(logoutRequest request) { }

        public static void sendCustomerTransactionReceiptRequest(sendCustomerTransactionReceiptRequest request) { }

        public static void ARBGetSubscriptionListRequest(ARBGetSubscriptionListRequest request) { }
    
        public static void XXDoNotUseDummyRequest(XXDoNotUseDummyRequest request)
        {
            if (null != request)
            {
                paymentSimpleType(request.paymentSimpleType);
            }
        }

    }
// ReSharper restore InconsistentNaming
#pragma warning restore 1591
#pragma warning restore 169
}
 