namespace NationalArchives.AdvancedOrders.BusinessObjects
{
    public class DocumentReference
    {
        public DocumentParts DocParts { get; set; }

        public DocumentRefCodes ReturnStatus { get; set; }

        public DocumentReference()
        {
            DocParts = new DocumentParts();
            ReturnStatus = new DocumentRefCodes();
        }
    }

    public class DocumentParts
    {
        public int PieceId { get; set; }

        public int ItemId { get; set; }

        public string DocumentReferenceText { get; set; }

        public int SubClass { get; set; }

        public string PieceRef { get; set; }

        public string ItemRef { get; set; }

        public string ClosureStatus { get; set; }

        public string Scope { get; set; }

        public int ReturnStatus { get; set; }

    }

    public enum DocumentRefCodes
    {
        DOCUMENT_REFERENCE_VALID = 0,
        DOCUMENT_REFERENCE_INVALID_LETTER_CODE_OR_CLASS = -1,
        DOCUMENT_REFERENCE_INVALID_SUBCLASS = -2,
        DOCUMENT_REFERENCE_NO_ROWS_FOUND = -3,
        DOCUMENT_REFERENCE_MULITIPLE_ROWS_FOUND = -4,
        DOCUMENT_REFERENCE_INVALID_LETTER_CODE = -5,
        DOCUMENT_REFERENCE_UN_ORDERABLE = -6,
        DOCUMENT_REFERENCE_CLOSED = -7,
        DOCUMENT_REFERENCE_OFFSITE = -8,
        DOCUMENT_REFERENCE_KEW_SURROGATE = -9,
        DOCUMENT_REFERENCE_UNFIT = -10,
        DOCUMENT_REFERENCE_MISSING = -11,
        DOCUMENT_REFERENCE_DIGITAL_SURROGATE_AVAILABLE = -12,
        DOCUMENT_REFERENCE_NOT_HELD_AT_KEW = -13,
        DOCUMENT_REFERENCE_RELOCATION = -14,
        DOCUMENT_REFERENCE_COLLECTION_CARE = -15,
        DOCUMENT_REFERENCE_ON_LOAN = -16,
        DOCUMENT_REFERENCE_ON_DISPLAY = -17,
        DOCUMENT_REFERENCE_ON_MOULD_TREATMENT = -18,
        DOCUMENT_REFERENCE_NO_PATTERN_MATCH = -19,
        DOCUMENT_REFERENCE_NULL = -20
    }

    public static class DocumentRefCodesExtension
    {
        public static string ToError(this DocumentRefCodes errorCode)
        {
            switch (errorCode)
            {
                case DocumentRefCodes.DOCUMENT_REFERENCE_VALID:
                case DocumentRefCodes.DOCUMENT_REFERENCE_MULITIPLE_ROWS_FOUND:
                case DocumentRefCodes.DOCUMENT_REFERENCE_OFFSITE:
                    return string.Empty; ;
                case DocumentRefCodes.DOCUMENT_REFERENCE_INVALID_LETTER_CODE_OR_CLASS:
                case DocumentRefCodes.DOCUMENT_REFERENCE_INVALID_SUBCLASS:
                case DocumentRefCodes.DOCUMENT_REFERENCE_NO_ROWS_FOUND:
                case DocumentRefCodes.DOCUMENT_REFERENCE_INVALID_LETTER_CODE:
                    return "This document cannot be identified. Check catalogue reference has been entered correctly.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_UN_ORDERABLE:
                case DocumentRefCodes.DOCUMENT_REFERENCE_UNFIT:
                case DocumentRefCodes.DOCUMENT_REFERENCE_MISSING:
                case DocumentRefCodes.DOCUMENT_REFERENCE_RELOCATION:
                case DocumentRefCodes.DOCUMENT_REFERENCE_COLLECTION_CARE:
                case DocumentRefCodes.DOCUMENT_REFERENCE_ON_LOAN:
                case DocumentRefCodes.DOCUMENT_REFERENCE_ON_MOULD_TREATMENT:
                    return "This document cannot be ordered. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_CLOSED:
                    return "This document is closed and cannot be ordered. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_KEW_SURROGATE:
                    return "Microfiche or microfilm documents do not need to be ordered. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_DIGITAL_SURROGATE_AVAILABLE:
                    return "Document is digitised and cannot be ordered. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_NOT_HELD_AT_KEW:
                    return "This document is held by another archive and cannot be viewed at The National Archives. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_ON_DISPLAY:
                    return "This document is on display and cannot be ordered. Check the catalogue for more information.";
                case DocumentRefCodes.DOCUMENT_REFERENCE_NO_PATTERN_MATCH:
                    return "This reference is not recognised. Check the catalogue for more information.";
                default:
                    return string.Empty;
            }
        }
    }
}
